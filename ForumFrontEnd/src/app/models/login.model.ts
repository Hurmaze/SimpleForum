export class LoginModel {
    Email: string;
    Password: string;
    constructor(email: string, password: string){
        this.Email = email;
        this.Password = password;
    }
}
