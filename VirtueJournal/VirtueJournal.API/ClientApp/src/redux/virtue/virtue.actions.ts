import axios from "axios";
import { Dispatch } from "redux";

import {
  Virtue,
  VirtueConstants,
  GetVirtuesAction,
  GetVirtueAction,
  VirtueForCreate,
  CreateVirtueAction
} from "./virtue.types";

import { getAuthHeader } from "../../helpers/get-auth-header";

const BASE_URL = "/api/virtues";
const authHeader = getAuthHeader();

export const getVirtues = () => async (dispatch: Dispatch) => {
  try {
    const res = await axios.get<Virtue[]>(BASE_URL, {
      headers: authHeader
    });

    dispatch<GetVirtuesAction>({
      type: VirtueConstants.GET_VIRTUES,
      payload: res.data
    });
  } catch (err) {
    throw new Error(err);
  }
};

export const getVirtue = (virtueId: number) => async (dispatch: Dispatch) => {
  try {
    const res = await axios.get<Virtue>(`${BASE_URL}/${virtueId}`);

    dispatch<GetVirtueAction>({
      type: VirtueConstants.GET_VIRTUE,
      payload: res.data
    });
  } catch (err) {
    throw new Error(err);
  }
};

export const createVirtue = (virtueToCreate: VirtueForCreate) => async (
  dispatch: Dispatch
) => {
  try {
    const res = await axios.post<Virtue>(BASE_URL, virtueToCreate, {
      headers: authHeader
    });

    dispatch<CreateVirtueAction>({
      type: VirtueConstants.CREATE_VIRTUE,
      payload: res.data
    });
  } catch (err) {
    throw new Error(err);
  }
};
