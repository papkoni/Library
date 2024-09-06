import { makeAutoObservable } from "mobx";
import { getAccessToken, getRefreshToken } from "./tokens";
import { checkAccess, handleRefreshToken } from "../utils/api/authApi";

export default class UserStore {
  constructor() {
    this.who = null;
    this._isAuth = false;
    this._user = {};
    this.isLoading = true; // Флаг загрузки
    makeAutoObservable(this);

   
  }

  

  setIsAuth(bool) {
    this._isAuth = bool;
  }

  setUser(user) {
    this._user = user;
    this.updateRole();
  }

  setLoading(bool) {
    this.isLoading = bool;
  }

  updateRole() {
    this.who = this._user.Role || null;
  }

  get isAuth() {
    return this._isAuth;
  }

  get user() {
    return this._user;
  }

  get loading() {
    return this.isLoading;
  }
}
