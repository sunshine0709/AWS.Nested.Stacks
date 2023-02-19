using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;

namespace AWS.Nested.Stacks;

public class Repository
{
    private readonly IAmazonDynamoDB _amazonDynamoDB;
    private readonly DynamoDBContext _context;
    private readonly ILambdaLogger _logger;

    public Repository(ILambdaLogger logger)
    {
        AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig
        {
            RegionEndpoint = RegionEndpoint.APSoutheast2
        };
        var dynamoDBContextConfig = new DynamoDBContextConfig
        {
            ConsistentRead = true
        };

        _amazonDynamoDB = new AmazonDynamoDBClient(clientConfig);

        _context = new DynamoDBContext(_amazonDynamoDB, dynamoDBContextConfig);

        _logger = logger;
    }

    public async Task<bool> SaveAsync(Item item)
    {
        try
        {
            await _context.SaveAsync(item);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to save record to DynamoDb table : {ex.Message}");
            throw;
        }
    }
}

[DynamoDBTable("nested-stack-example")]
public class Item
{
    [DynamoDBHashKey(attributeName: "row_id")] //Partition key
    public string RowId { get; private set; } = Guid.NewGuid().ToString();

    [DynamoDBProperty(attributeName: "input")]
    public string? Input { get; set; }

    [DynamoDBProperty(attributeName: "updated")]
    public string? Updated { get; private set; } = DateTime.Now.ToString();
}

