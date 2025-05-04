# Rezerwacja sal
## Wymagania
- Net SDK 8 + https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Baza danych SQLServer
## Kroki uruchomieniowe programu
- W pliku RoomReservation/appsettings.json należy odnaleść linię "DefaultConnection": "" i uzupełnić w niej dane dostępowe do serwera bazodanowego
- Następnie należy wykonać aktualizację bazy danych za pomocą komendy w konsoli CMD dla katalogu \RoomReservation
    
 dotnet ef database update 
 
 - jeżeli nie istnieje narzędzie dotnet ef należy je najpierw zainstalować za pomocą komendy

dotnet tool install --global dotnet-ef   
 
- Następnie by uruchomić program należy użyć komendy 

dotnet run
