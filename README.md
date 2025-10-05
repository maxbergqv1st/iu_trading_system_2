# IU Trading System 2

# Usings

- Registrera dig och logga in för att att kolla om det finns någon som vill tradea med dig xD.
- För att röra dig runt i programet är tydligt och kräven ENTER efter valet i [x] skrivits.
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
- Det gör också att [POLYMORPHISM] kan möjliggöra framtida användare som t.ex. admin osv. Metoden "TryLogin" kan implementeras olika men kallas på samma sätt och på så sätt minimera kod.
- Trade innehåller listor av items som egna objekt. Systemet använder [Composition]
