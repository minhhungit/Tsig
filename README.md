# Tsig
[Twitter Snowflake-alike Id Generator] Demo Application

Build with :heart: NancyFx - Topshelf - AppCfg.Net - NLog - SimpleInjector and [IdGen](https://github.com/RobThree/IdGen)

### Settings

```xml
<appSettings>
   <add key="AppName" value="TsigApp"/>
   <add key="AppDisplayName" value="Tsig - Id Generator Application"/>
    
   <add key="NanycyEndpointSchema" value="http" />
   <add key="NanycyEndpointDomain" value="localhost" />
   <add key="NanycyEndpointPort" value="8111" />
   <add key="NanycyEndpointPath" value="tsig/" />
   <add key="NancyEndpointApiKey" value="4E1B875F-E091-4510-81DD-9334FBE98FDC" />

   <add key="IdGenGeneratorId" value="1"/>
   <add key="IdGenEposhUtc" value="2019,01,01,00,00,00"/>
   <add key="IdGenMaskConfig" value="42,7,14"/>
</appSettings>
```

### Run
- Build and run TsigApp.exe 
- Or host it as windows service by [topshelf](http://docs.topshelf-project.com/en/latest/overview/commandline.html) command: 
  > TsigApp install --autostart

  > TsigApp start
  
- Send request to http://localhost:8111/tsig/gen/id with ApiKey
- Receive Id 

### Screenshot
<img src="https://raw.githubusercontent.com/minhhungit/Tsig/master/wiki/demo01.png" />

<img src="https://raw.githubusercontent.com/minhhungit/Tsig/master/wiki/demo02.png" />
