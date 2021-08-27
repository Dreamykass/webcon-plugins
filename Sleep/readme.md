This action sleeps for some configurable amount of seconds

Its purpose is to show that webcon executes actions asynchronously - 
while this plugin action is sleeping, the user can switch 
to a different task/workflow/document/application, and the whole system *does not* freeze up.

The consequence of the above, is that it's possible to execute arbitrary blocking and synchronous actions, 
like a get/post request to some web api.
