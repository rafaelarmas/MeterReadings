# MeterReadings

Multi-layered React JS and .Net Core solution which allows a CSV file with meter readings to be uploaded to a database. Client accounts are seeded in to the database and meter readings must pass validation to be successfully recorded.

### Sample meter reading CSV:
```
AccountId,MeterReadingDateTime,MeterReadValue
2344,22/04/2019 09:24,01002
2233,22/04/2019 12:25,00323
8766,22/04/2019 12:25,03440
```

#### Useful database commands
```
dotnet ef database update InitialCreate
```
