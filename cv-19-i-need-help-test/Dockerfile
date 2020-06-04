FROM mcr.microsoft.com/dotnet/core/sdk:2.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./cv-19-i-need-help.sln ./
COPY ./cv-19-i-need-help/cv-19-i-need-help.csproj ./cv-19-i-need-help/
COPY ./cv-19-i-need-help-test/cv-19-i-need-help-test.csproj ./cv-19-i-need-help-test/

RUN dotnet restore ./cv-19-i-need-help/cv-19-i-need-help.csproj
RUN dotnet restore ./cv-19-i-need-help-test/cv-19-i-need-help-test.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out ./cv-19-i-need-help-test/cv-19-i-need-help-test.csproj

CMD dotnet test