export interface ErrorState {
  errorMessage: string
}

export enum ErrorConstants {
  GET_ERRORS = "GET_ERRORS",
  CLEAR_ERRORS = "CLEAR_ERRORS"
}

export interface GetErrorsAction {
  type: ErrorConstants.GET_ERRORS,
  payload: string
}

export interface ClearErrorsAction {
  type: ErrorConstants.CLEAR_ERRORS;
}

export type ErrorAction = GetErrorsAction | ClearErrorsAction;