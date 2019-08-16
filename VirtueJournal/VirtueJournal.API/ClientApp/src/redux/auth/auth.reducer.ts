import { AuthTypes, AuthAction, AuthState, User } from "./auth.types";

let user;
const userJson = localStorage.getItem("user");
if (userJson) {
  user = JSON.parse(userJson) as User;
}

const initialState: AuthState = user
  ? { currentUser: user, isLoggedIn: true }
  : { isLoggedIn: false };

export const authReducer = (state = initialState, action: AuthAction) => {
  switch (action.type) {
    case AuthTypes.LOGIN_SUCCESS:
      return {
        isLoggedIn: true,
        user: action.payload
      };
    case AuthTypes.LOGIN_FAILURE:
      return {
        isLoggedIn: false,
        user: null
      };
    case AuthTypes.LOGOUT:
      return {
        isLoggedIn: false,
        user: null
      };
    default:
      return state;
  }
};
