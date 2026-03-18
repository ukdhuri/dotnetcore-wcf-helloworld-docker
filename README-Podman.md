# Running CoreWCF Hello World with Podman

This project can be compiled and run seamlessly using [Podman](https://podman.io/), which serves as a daemonless, 1:1 alternative to Docker.

## Step 1: Build the Image
Ensure you are in the directory containing the `Dockerfile` and `HelloWorldWcf.csproj`.
```bash
podman build -t helloworldwcf:latest .
```

## Step 2: Run the Container
By default, the .NET 8 ASP.NET Core image exposes port `8080` internally. Map localhost's port `5000` to the container's `8080`.
```bash
podman run -d -p 5000:8080 --name wcf-service-podman helloworldwcf:latest
```

## Step 3: Test the Service
You can test the SOAP endpoint via `curl` now that the container is running.

**Check WSDL Accessible:**
```bash
curl -s http://localhost:5000/HelloWorldService/basichttp?wsdl
```

**SOAP POST Request:**
Make sure you have a `request.xml` payload, then:
```bash
curl -s -X POST \
  -H 'Content-Type: text/xml; charset=utf-8' \
  -H 'SOAPAction: "http://tempuri.org/IHelloWorldService/SayHello"' \
  -d @request.xml \
  http://localhost:5000/HelloWorldService/basichttp
```
