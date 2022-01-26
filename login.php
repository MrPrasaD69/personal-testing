
<html lang="en_US">
    <head>
        <meta charset ="UTF-8">
        <title> Admin Login </title>
        
    </head>
    <body>
        <h1 align="center">Admin Login</h1><br>
        <form action="validation.php" method="post">
            
            <table align="center">
                <tr>
                    <td>Username</td><td><input type="text" name="name"></td>
                </tr>
                <tr>
                    <td>Password</td><td><input type="password" name="pass"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><input type="submit" name="login" value="Login"></td>
                </tr>
                
            </table>
            
        </form>

                <h1 align="center">User Register</h1><br>
        <form action="validation.php" method="post">
            
            <table align="center">
                <tr>
                    <td>Username</td><td><input type="text" name="name"></td>
                </tr>
                <tr>
                    <td>Password</td><td><input type="password" name="pass"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><input type="submit" name="register" value="Register"></td>
                </tr>
                
            </table>
            
        </form>
    </body>
</html>

