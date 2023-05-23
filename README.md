# Contacts WPF Demo

This is a quick WPF test app for a very minimalistic contacts manager.
I used this to learn and test some basics of using WFP in a matter of hours, without having used it before
or having experience with what the best practices when working with WPF are.

Built with VS 2022 and .NET 6.

## What it currently does

- A simple overview of contacts (Name, Address, Email, Phone Number) in a DB
- Viewing and editing these contacts
- Adding and deleting contacts
- Saving occurs automatically in the background
- The DB file is a simple JSON file in the current working directory
- Loading happens when starting the app
- Localization files for English (default) and German (de-DE) are included. German should be used automatically
  on German systems.
- Searching through the contacts
- Contacts are sorted alphabetically by Name

## What it does NOT do (yet)

Here are a selection of notes on what the app does not do but that I considered along the way and
what might be worth looking into when working on it further.

- No Validation. All contact fields are unchecked strings and the user can enter whatever they want.
  Validation to ensure specific formats could be added for things like E-Mail addresses and phone numbers.
- No custom fields. Depending on what a user may want to store for contacts, a fully dynamic contact with a collection
  of an unlimited number of entries/fields that users can add and remove on demand would be considerable.
- Manual saving is not required. If that was an explicit goal of the app instead of binding to contact instances
  directly, an additional layer for temporary data that can be used or discarded before being persisted should be added.
- No special styling. The looks of the app are default WPF. Since my system usually runs with dark mode themes, something
  like light/dark-mode detection as well as manual options to switch between themes would be cool.
- Database file is hardcoded. In a more realistic app, options to load and save specific files or to customize the default path
  and provide options to move existing DBs would be nice.
- Localization should be used based on the system language but there is no way to manually switch. Adding a way to change localization
  by user preference for the app rather than system preference would be better.
- Modularization could be improved. Contact editing is just binding to a DataTemplate for the type and button clicks are handled by
  handlers in the MainWindow. Ideally that kind of logic would fit better alongside the corresponding views. Things to consider here are:
  - Using an explicit ViewModel for the Contact Editor
  - Moving the XAML design to separate files, maybe with resource files or by creating User Controls.
  - Moving the click handling to their own view models and implementing WPF Commands with CommandParameters.
