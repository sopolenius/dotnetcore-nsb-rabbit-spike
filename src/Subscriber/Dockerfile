# SUBSCRIBER
FROM microsoft/dotnet:2.0-runtime
WORKDIR /app
COPY /start.sh .
COPY /wait-for-it.sh .
COPY /bin/Debug/netcoreapp2.0/publish .
ENTRYPOINT ["sh", "./start.sh"]
