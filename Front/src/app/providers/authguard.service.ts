import { Injectable } from "@angular/core";
import { Router, CanActivate } from '@angular/router';
import { Global } from "../providers/global.service";
import { LoginModel } from '../models/login.model';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(public router: Router,
    public global: Global) { }

  canActivate(): boolean {
    const token = this.global.getLoggedUser().accessToken;
    const expiration = this.global.getLoggedUser().expiration;
    const expirationDate = new Date(expiration);
    const now = new Date();
    if (!token || token.trim() === '' || now > expirationDate) {
      this.router.navigate(['login']);
      this.global.setLoggedUser(new LoginModel());
      return false;
    }
    return true;
  }
}
