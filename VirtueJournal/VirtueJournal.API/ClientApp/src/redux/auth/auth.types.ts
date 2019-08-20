export enum AuthConstants {
  LOGIN_SUCCESS = "LOGIN_SUCCESS",
  LOGIN_FAILURE = "LOGIN_FAILURE",
  LOGOUT = "LOGOUT"
}

export interface User {
  userId: number,
  username: string,
  firstName: string,
  lastName: string,
  token: string
}

export interface UserForRegister {
  firstName: string,
  lastName: string,
  email: string,
  username: string,
  password: string
}

export interface AuthState {
  currentUser?: User,
  isLoggedIn: boolean
}

/* ACTIONS */
export interface LoginSuccessAction {
  type: AuthConstants.LOGIN_SUCCESS;
  payload: User;
}

export interface LoginFailureAction {
  type: AuthConstants.LOGIN_FAILURE
}

export interface LogoutAction {
  type: AuthConstants.LOGOUT;
}

export type AuthAction = LoginSuccessAction | LoginFailureAction | LogoutAction;