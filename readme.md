# I Need Help Admin API

The I Need Help Admin API provides access to the annex records from the resident support database and allows clients to:
- Search the annex table
- Access and update resident requests
- Generate delivery schedules

## Stack

- .NET Core as a web framework.
- nUnit as a test framework.

## Contributing
- Selwyn Preston
- Rashmi Shetty
- Emma Corbett
- Alex Demetriou

### Setup

1. Install [Docker][docker-download].
2. Install [AWS CLI][AWS-CLI].
3. Clone this repository.
5. Open it in your IDE.

### Development

This application is a series of Lambda functions that need to be deployed to an AWS account for the best experience but can also be run locally.

Serve locally using docker:
1.  Add you security credentials to AWS CLI.
```sh
$ aws configure
```
2. Log into AWS ECR.
```sh
$ aws ecr get-login --no-include-email
```
3. Build and serve the application. It will be available in the port 3000.
```sh
$ docker-compose build cv-19-i-need-help && docker-compose up cv-19-i-need-help
```

### Release process

We use a pull request workflow, where changes are made on a branch and approved by one or more other maintainers before the developer can merge into `master` branch.

Then we have an automated deployment process, which runs in CircleCI.

1. Automated tests (nUnit) are run to ensure the release is of good quality.
2. The application is deployed to development automatically, where we check our latest changes work well.
3. We manually confirm a production deployment in the CircleCI workflow once we're happy with our changes in staging.
4. The application is deployed to production.

Our development and production environments are hosted by AWS. We would deploy to production per each feature/config merged into  `master`  branch.

## Testing

### Run the tests

```sh
$ docker-compose up test-database & docker-compose build cv-19-i-need-help-test && docker-compose up cv-19-i-need-help-test
```

To run database tests locally (e.g. via Visual Studio) the `CONNECTION_STRING` environment variable will need to be populated with:

`Host=localhost;Database=entitycore;Username=postgres;Password=mypassword"`

Note: The Host name needs to be the name of the stub database docker-compose service, in order to run tests via Docker.


### Active Maintainers

- **Selwyn Preston**, Lead Developer at London Borough of Hackney (selwyn.preston@hackney.gov.uk)
- **Mirela Georgieva**, Developer at London Borough of Hackney (mirela.georgieva@hackney.gov.uk)
- **Matt Keyworth**, Developer at London Borough of Hackney (matthew.keyworth@hackney.gov.uk)

### Other Contacts

- **Rashmi Shetty**, Development Manager & Product Owner at London Borough of Hackney (rashmi.shetty@hackney.gov.uk)

[docker-download]: https://www.docker.com/products/docker-desktop
[AWS-CLI]: https://aws.amazon.com/cli/
