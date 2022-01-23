## Technologies

- .NET 5.0
- SWAGGER
- DOCKER
- JWT Auth
- Dapper MSSQL

## Dependencies
```
- CryptoHelper > 3.0.2
- Dapper > 2.0.123
- Authentication.JwtBearer 5.0.0
 ```
## Setup Notes



## Scipts

```sql
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Balance] [decimal](4, 2) NULL,
	[Currency] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](150) NULL,
	[AccountType] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Surname] [nvarchar](150) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
 

CREATE TABLE [dbo].[Trnx](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](150) NULL,
	[AccountId] [int] NULL,
	[Amount] [decimal](4, 2) NULL,
	[RequestDate] [datetime] NULL,
 CONSTRAINT [PK_Trnx] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](250) NULL,
	[Hash] [nvarchar](500) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](150) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 





```



## Responses

### Success
 ```Json
{
  "customers": [
    {
      "name": "Murat",
      "surname": "Güzel"
    },
    {
      "name": "John",
      "surname": "Doe"
    }
  ],
  "header": {
    "statusCode": 0,
    "message": "Success"
  }
}
 
``` 
### Failed
 ```Json
{
  "statusCode": 1001,
  "message": "Insufficient Balance"
}
 
``` 

## Endpoints

![Ekran Alıntısı1](https://user-images.githubusercontent.com/28257096/150682418-867cb0a2-e2e8-4b10-b179-c7a803af1c32.PNG)
