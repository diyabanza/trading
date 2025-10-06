--- ANVÄNDNING AV PROGRAM ---

När du startar programmet kommer du till huvudmenyn.

- Här kan du logga in eller registrera dig:

Skriv "1" för att logga in:
Då fyller du i ditt användarnamn och lösenord.
Om det stämmer så loggas du in.

Skriv "2" för att registrera ett nytt konto
Ange ett användarnamn och lösenord.
Kontot sparas även så att du kan logga in nästa gång.

Skriv "quit" för att stänga programmet.

- När du är inloggad får du en meny med följande val:

1: Upload an item:
Här lägger du upp en ny sak för byte. Du anger namn och beskrivning samt bekräftar uppladdningen.

2: View list of other users items:
Här ser du alla saker som andra användare lagt upp.
Du kan välja ett objekt för att se mer information och eventuellt skicka en trade request.

3: Browse trade requests:
Se mottagna förfrågningar (gå till Trade Process nedan).
Se skickade förfrågningar.
Se godkända trades.
Se nekade trades.
Logga ut och återgå till huvudmenyn.

(Trade Process)
När du ser ett item som du vill tradea till dig:
Välj itemet.
Välj också vilket av dina egna items du vill byta med.
Bekräfta trade requesten.
Ägaren får då en förfrågan och kan acceptera eller neka den.



--- IMPLEMENTATIONSVAL ---

Komposition:
Jag använde komposition eftersom objekten är separata entiteter.
Här används klasserna User, Item, Trade (Tradestatus) och DataStorage som innehåller data och funktionalitet för just det objektet.
Det gör det enkelt att lägga till nya funktioner för ett objekt utan att påverka andra delar av programmet.

Arv:
Jag valde att inte använda arv eftersom jag inte har tydliga hierarkier som kräver det.
Eftersom varje klass är ganska självständig blir komposition enklare och kodbasen blir mer flexibel.

Errorhantering:
Jag använde try/catch för att undvika att programmet kraschar vid fel input.

Menystruktur:
Här används en loop med tydliga menyval så att användaren enkelt kan navigera.
