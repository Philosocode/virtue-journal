import { GetErrorsAction, ClearErrorsAction, ErrorConstants } from "./error.types";

export const getErrors = (errorMessage: string): GetErrorsAction => ({
  type: ErrorConstants.GET_ERRORS,
  payload: errorMessage
});

export const stopLoading = (): ClearErrorsAction => ({
  type: ErrorConstants.CLEAR_ERRORS
});