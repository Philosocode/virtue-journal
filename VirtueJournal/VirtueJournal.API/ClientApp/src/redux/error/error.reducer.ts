import { ErrorState, ErrorConstants, ErrorAction } from "./error.types";

const initialState: ErrorState = {
  errorMessage: ""
};

export const errorReducer = function(
  state = initialState,
  action: ErrorAction
): ErrorState {
  switch (action.type) {
    case ErrorConstants.GET_ERRORS:
      return {
        errorMessage: action.payload
      };
    case ErrorConstants.CLEAR_ERRORS:
      return {
        errorMessage: ""
      };
    default:
      return state;
  }
};
