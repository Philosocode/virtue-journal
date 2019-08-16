export enum AuthTypes {
  LOGIN_SUCCESS = "LOGIN_SUCCESS",
  LOGIN_FAILURE = "LOGIN_FAILURE",
  LOGOUT = "LOGOUT"
}

export interface User {
  userId: number,
  userName: string,
  firstName: string,
  lastName: string,
  token: string
}

export interface AuthState {
  currentUser?: User,
  isLoggedIn: boolean
}

export interface LoginSuccessAction {
  type: AuthTypes.LOGIN_SUCCESS;
  payload: any;
}

export interface LoginFailureAction {
  type: AuthTypes.LOGIN_FAILURE,
  payload: any
}

export interface LogoutAction {
  type: AuthTypes.LOGOUT;
}

export type AuthAction = LoginSuccessAction | LoginFailureAction | LogoutAction;