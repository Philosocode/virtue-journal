import { SetErrorAction, ClearErrorAction, ErrorConstants } from "./error.types";

export const setError = (errorMessage: string): SetErrorAction => ({
  type: ErrorConstants.SET_ERROR,
  payload: errorMessage
});

export const clearError = (): ClearErrorAction => ({
  type: ErrorConstants.CLEAR_ERROR
});