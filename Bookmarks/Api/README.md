# Developer setup
This project depends on RabbitMQ. You can run any of the official images, more info here:
- https://hub.docker.com/_/rabbitmq
- https://rabbitmq.com/

This command runs a rabbitmq broker with management interface (on http://localhost:15672). It also creates a user and password through environment variables.

Warning: the setup shown here is great for testing and developing, not for production systems.
## Manual
```bash
docker run \
  -e RABBITMQ_DEFAULT_USER=user \
  -e RABBITMQ_DEFAULT_PASS=PASSWORD \
  -p 15672:15672 \
  -p 5672:5672 \
  rabbitmq:3-management-alpine
```

## Docker Compose
```bash
# Up
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up
```

### Jetbrains Rider
For Jetbrains Rider we have the following Run/Debug configuration stored with the source code.
![run debug configuration](C:\Users\yurib\source\repos\amped\Code\rundebug.png "run debug configuration")

### Visual Studio
For Visual Studio we can set the Docker Compose as the default startup project. No further launch configuration required.



