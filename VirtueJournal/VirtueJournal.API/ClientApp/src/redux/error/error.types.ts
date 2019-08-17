export interface ErrorState {
  errorMessage: string
}

export enum ErrorConstants {
  SET_ERROR = "SET_ERROR",
  CLEAR_ERROR = "CLEAR_ERROR"
}

export interface SetErrorAction {
  type: ErrorConstants.SET_ERROR,
  payload: string
}

export interface ClearErrorAction {
  type: ErrorConstants.CLEAR_ERROR;
}

export type ErrorAction = SetErrorAction | ClearErrorAction;