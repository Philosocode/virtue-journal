import axios from "axios";
import { Dispatch } from "redux";

import {
  AuthConstants,
  LoginSuccessAction,
  LoginFailureAction,
  LogoutAction,
  UserForRegister,
  User
} from "./auth.types";

import { startLoading, stopLoading } from "../loading";

const BASE_URL = "/api/auth";

export const registerUser = async (user: UserForRegister) => {
  return axios.post(`${BASE_URL}/register`, user);
}

export const loginUser = (username: string, password: string) => async (dispatch: Dispatch) => {
  dispatch(startLoading());

  const loginData = { username, password };

  try {
    const res = await axios.post<User>(`${BASE_URL}/authenticate`, loginData);
    const userData = res.data;
    
    // store user details and jwt token in local storage
    // to keep user logged in between page refreshes
    localStorage.setItem("user", JSON.stringify(userData));

    dispatch<LoginSuccessAction>({
      type: AuthConstants.LOGIN_SUCCESS,
      payload: userData
    });
  } catch (err) {
    dispatch<LoginFailureAction>({ type: AuthConstants.LOGIN_FAILURE });
    throw new Error(err);
  } finally {
    dispatch(stopLoading());
  }
};

export const logoutUser = (): LogoutAction => {
  localStorage.removeItem("user");

  return { type: AuthConstants.LOGOUT };
};
