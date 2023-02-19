using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWS.Nested.Stacks;

/*
 * References
https://docs.aws.amazon.com/prescriptive-guidance/latest/patterns/automate-deployment-of-nested-applications-using-aws-sam.html
*/
public class Function
{
    /// <summary>
    /// Function to add user input into a DynamoDb table.......
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandlerAsync(string input, ILambdaContext context)
    {
        //https://github.com/ziedbentahar/aws-parameters-and-secrets-lambda-extension-sample/tree/main/aws/cfn
        try
        {
            context.Logger.LogLine($"Initiating FunctionHandler -> input parameter : {input}");

            var repo = new Repository(context.Logger);
            var item = new Item
            {
                Input = input
            };

            await repo.SaveAsync(item);

            context.Logger.LogLine($"Input successfully saved to DynamoDb -> Guid : {item.RowId}");

            return input.ToUpper();
        }
        catch (Exception ex)
        {
            context.Logger.LogLine(ex.Message);
            throw;
        }
    }
}