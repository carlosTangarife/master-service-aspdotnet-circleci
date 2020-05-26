FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Renting.MasterServices.Api/Renting.MasterServices.Api.csproj Renting.MasterServices.Api/
COPY Renting.MasterServices.Domain/Renting.MasterServices.Domain.csproj Renting.MasterServices.Domain/
COPY Renting.MasterServices.Infraestructure/Renting.MasterServices.Infraestructure.csproj Renting.MasterServices.Infraestructure/
COPY Renting.MasterServices.Core/Renting.MasterServices.Core.csproj Renting.MasterServices.Core/
RUN dotnet restore Renting.MasterServices.Api/Renting.MasterServices.Api.csproj
COPY . .
WORKDIR /src/Renting.MasterServices.Api
RUN dotnet build Renting.MasterServices.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Renting.MasterServices.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Renting.MasterServices.Api.dll"]
