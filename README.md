# Introduction
This is the new LEGO ID 4 Control system (Demo)

# Development
The solution is developed in .NET 8 using vertical slice architecture where different areas communicate using the Mediator pattern.

## Local development
Open the solution using your IDE/Editor of choice. The solution have been tested with Rider.

## Tests
We are following a honeycomb test strategy with a few  E2E tests, a lot of integration tests and a few unit tests.
All tests can be found under the test folder, and are build using xunit. 

# Deployment
Build the docker image by running `docker build .` from the "LEGO4IDControl" folder under `src`, it is then possible to deploy the solution to any platform that supports Docker.