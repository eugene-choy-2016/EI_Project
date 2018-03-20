<!DOCTYPE html>
<html>
<head>
    <title>EI EI O</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Helvetica Neue', 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', sans-serif;
        }

        #hero {
            background-color: #000;
            height: 100vh;
        }

        #subhero-1 {
            background: url("web/hero.jpg"), #000;
            background-position: 20% 100%;
            background-size: 120%;
        }

        #subhero-2 {
            background-color: #fff;
            height: 100vh;
            padding-left: 2em;
            padding-right: 2em;
        }
    </style>
</head>
<body>
    <div id="hero" class="container-fluid p-0">
        <div class="row m-0">
            <div id="subhero-1" class="col-sm-8">
            </div>
            <div
            <div id="subhero-2" class="col-sm-4 d-flex align-items-center">
                <div class="col-sm-12">
                    <h1>Welcome</h1>
                    <p>Please login to access the schedule</p>
                    <form id="login">
                        <div class="form-group">
                            <label for="username">Username</label>
                            <input id="username" class="form-control" type="text" placeholder="Enter Username" required/>
                        </div>
                    
                        <div class="form-group">
                            <label for="password">Password</label>
                            <input id="password" class="form-control" type="password" placeholder=" Enter Password" required/>
                        </div>
                    
                        <button type="submit" class="btn btn-error" disabled>Login</button>
                    </form>
                </div>

            </div>
        </div>
    </div>
</body>
</html>