#!/bin/sh
#while ! curl -f http://rabbitmq:15672/; do sleep 3; done
./wait-for-it.sh rabbitmq:5672 -t 30
dotnet Subscriber.dll
