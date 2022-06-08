Multi search hits counter

Info:
 - Application created based on Asp.net Blazor server mode using net6.0 
 - App created with Rider IDE, but tested with VisualStudio 2022 also 
 - Three search providers are available: Bing, Wikipedia and Github topics. For Github and Wikipedia api calls are used, html parsing is used for Bing. 
 

Run instructions:

 - Open solution with any IDE (VS2022, Rider, VScode) 
 - Build and Run
 
 Assumptions & considerations:
 - Search requests are executed in parallel mode and returns their results as soon as they available 
 - UI has "engineering design" and potentially could be improved 
 - Logging is available and set to console by default 