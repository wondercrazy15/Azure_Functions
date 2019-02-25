Implementation Microsoft Azure Functions with SendGrid Mail

Here we have created two project 
1. AzureFunctionWithSendGrid
2. CalcApp


1. AzureFunctionWithSendGrid:
    In this Application, We have created one Azure Function with the name of SendMail. In the function, we have written code for sending mail through the SendGrid. For that, we have also used a SendGrid API Key. This API Key will be available in your SendGrid account

2. CalcApp:
       This is a simple web application in.Net. In this Application, we have simply implemented Form and take the input parameter like your email id and email subject line and pass the data to Azure Function throw the API call.

