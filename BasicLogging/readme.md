

this custom action should generate logs something like this:

```
BasicLoggingAction here hello
attachments count: 3
now logging all the attachments:
filename: Wniosek_..._.docx; extension: .docx; content.length (bytes): 29889; description: 
filename: Wniosek_..._.pdf; extension: .pdf; content.length (bytes): 51524; description: Generated from Wniosek_..._.docx
Culture: en-US
Duration: 13.2615ms
```

logging is done via `args.Context.PluginLogger.AppendInfo`, and so the logs can be found on the plugin package screen, in the designer studio
