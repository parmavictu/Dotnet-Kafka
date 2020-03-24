FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
#workdir cria e vai para a pasta
WORKDIR /app 


COPY . ./
#Quando ter mais de um csproj precisar  informar o nome
RUN dotnet restore
#Run é como abrir o terminal do linux
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "KafkaHandsOn.Consumer.dll"]