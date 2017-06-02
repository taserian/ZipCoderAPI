FROM microsoft/dotnet:latest
MAINTAINER aajrodriguez <a.a.j.rodriguez@gmail.com>
##RUN apt update && apt upgrade -y
RUN git clone https://github.com/taserian/ZipCoderAPI.git ZipCoderAPI
WORKDIR ZipCoderAPI
RUN dotnet restore
EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
