# AWS Nested Stack Project

A sample project that deploys an AWS lambda function and a DynamoDb table using nested stacks in SAM.
All resources are deployed using a nested stack. The nested stack is split into two resources;
- Stack 1 -> The DynamoDb resource.
- Stack 2 -> The Lambda resource.

## Install using AWS SAM CLI:

Once you have edited your template and code you can deploy your application using the [AWS SAM CLI Tool](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/serverless-sam-cli-command-reference.html) from the command line.

Install AWS SAM CLI if not already installed.
```
    https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html
```

If already installed check if new version is available.
```
    Follow steps to update cli version
    https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html

    Verify the installation.
    $ sam --version
```

Build & Deploy resources to AWS
```
    navigate to the CloudFormation folder in command prompt
    $ cd "AWS.Nested.Stacks/CloudFormation"

    Build package
    $ sam build -t application-stack.yml

    Deploy package
    Note: Once the package has been built... you will need to navigate to the newly created build folder, which shoul dbe found ./.aws-sam/build

    $ cd .aws-sam\build
    The exit deployment into CloudFormation in AWS... running either statement.

    $ sam deploy -t template.yaml --stack-name nested-stack-poc-jono --capabilities CAPABILITY_IAM CAPABILITY_AUTO_EXPAND

    OR 

    $ sam deploy -t template.yaml -g --capabilities CAPABILITY_IAM CAPABILITY_AUTO_EXPAND
```
