# CoreWCF Hello World Service

This is a .NET Core 8.0 WCF Hello World Service built with `CoreWCF`. It exposes a SOAP endpoint with a `SayHello` operation.

---

## 1. Running on Windows Locally

### Step 1: Install Required Software
1. Download the **.NET 8.0 SDK** installer for Windows from the official Microsoft website: [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Run the downloaded executable (e.g., `dotnet-sdk-8.X.X-win-x64.exe`) and follow the installation prompts.
3. Open a new Command Prompt or PowerShell window and verify the installation by running `dotnet --version`.

### Step 2: Run the Application
1. Open Command Prompt or PowerShell and navigate to the project directory containing `HelloWorldWcf.csproj`.
2. Run the following command:
   ```cmd
   dotnet run --urls=http://localhost:5000
   ```
3. The service will compile and start listening on `http://localhost:5000`. You can test it using the `curl` examples below or visual clients like SoapUI.

---

## 2. Running on Linux Locally

### Step 1: Install Required Software
You can install the .NET 8.0 SDK via your package manager or the official install script.
**Using the install script (Universal Linux):**
```bash
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh -c 8.0

# Add dotnet to your PATH
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$HOME/.dotnet
```
Alternatively, check your Linux distribution's package manager (e.g. `sudo apt-get install -y dotnet-sdk-8.0`).

### Step 2: Run the Application
1. Open a terminal and navigate to the project directory containing `HelloWorldWcf.csproj`.
2. Ensure you have the `dotnet` binary loaded in your path.
3. Run the following command:
   ```bash
   dotnet run --urls=http://localhost:5000
   ```
4. The `.NET Core` process will spool up and begin listening.

---

## 3. Running via Docker

### Step 1: Install Required Software
Ensure you have **Docker** installed on your system. 
- **Windows / Mac:** Install [Docker Desktop](https://www.docker.com/products/docker-desktop).
- **Linux:** Install your distro's `docker` or `docker.io` engine package, and ensure your user is in the `docker` group.

### Step 2: Build the Docker Image
1. Open your terminal or Command Prompt and navigate to the directory containing the `Dockerfile` and `HelloWorldWcf.csproj`.
2. Build the Docker image using the following command:
   ```bash
   docker build -t helloworldwcf:latest .
   ```

### Step 3: Run the Docker Container
Once the image is built, start a container. Since .NET 8 ASP.NET Core apps default to listening on port `8080` internal to the container, we map the host's port `5000` to the container's `8080`.
```bash
docker run -d -p 5000:8080 --name wcf-service helloworldwcf:latest
```

---

## 4. Running via Podman

Podman is a daemonless, daemon-free alternative to Docker. The commands are practically identical.

### Step 1: Build the Image
```bash
podman build -t helloworldwcf:latest .
```

### Step 2: Run the Container
```bash
podman run -d -p 5000:8080 --name wcf-service-podman helloworldwcf:latest
```

---

## Testing the Service (Curl)

Whether you are running the service natively (Windows/Linux) or via Docker, once it's listening on port `5000`, you can verify it from any terminal environment that has `curl`.

### 1. View Accessible WSDL
```bash
curl -s http://localhost:5000/HelloWorldService/basichttp?wsdl
```

### 2. SOAP Request (Test)
Save the following XML payload to a file named `request.xml`:
```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
  <s:Body>
    <SayHello xmlns="http://tempuri.org/">
      <name>World</name>
    </SayHello>
  </s:Body>
</s:Envelope>
```

Execute a SOAP POST request via `curl`:
```bash
curl -s -X POST \
  -H 'Content-Type: text/xml; charset=utf-8' \
  -H 'SOAPAction: "http://tempuri.org/IHelloWorldService/SayHello"' \
  -d @request.xml \
  http://localhost:5000/HelloWorldService/basichttp
```

### 3. Expected Output
```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
  <s:Body>
    <SayHelloResponse xmlns="http://tempuri.org/">
      <SayHelloResult>Hello, World!</SayHelloResult>
    </SayHelloResponse>
  </s:Body>
</s:Envelope>
```
