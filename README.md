# IU Trading System 2

# Usings

- Registrera dig och logga in för att att kolla om det finns någon som vill tradea med dig xD.
- Röra dig runt i programet med hjälp av de olika [meny] valen när ni kör programmet
- För att logga in och Accepta trades man gör med Username hasse1337 eller madmax. Har båda lösenordet: pass

# Funtioner

- Registrera och logga in användare.
- Ladda upp items med namn och beskrivning.
- Visa tradeable items från andra användare
- Skapa en trade-request mellan användare.
- Acceptera eller neka trade-request.
- Visa historik över genomförda och pending trades.

# Datalagring

- .txt filer som lagrar data lokalt som exempel på en databas.

# Varför

- Jag har använt mig av [INTERFACE] till IUser då jag vet att account ska ärva en gemensam struktur och kontrakt för att logga in.
- [POLYMORPHISM] kan möjliggöra framtida användare som t.ex. admin osv. Metoden "TryLogin" kan implementeras olika men kallas på samma sätt och på så sätt minimera kod.
- [Composition] har jag använt för att jag kunnat strukturera mina olika data nersparings-modeller till (Databasen/txt) och enkelt förstått när jag ska använda dom i mitt Program.cs

# FAQ

- Tryck [ENTER] efter input.
- Username: hasse1337 lösenord: pass
- Username: madmax lösenord: pass
