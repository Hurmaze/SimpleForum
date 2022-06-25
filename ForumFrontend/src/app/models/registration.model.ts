export class Registration {
    Email: string;
    Password: string;
    PasswordRepeat: string;
    RoleName: string;
    Nickname: string;
    constructor(email: string, password: string, password2: string, rolename: string, nickname: string){
        this.Email = email;
        this.Password = password;
        this.PasswordRepeat = password2;
        this.RoleName = rolename;
        this.Nickname = nickname;
    }
}
