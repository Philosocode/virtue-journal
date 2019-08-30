import axios from "axios";
import { Dispatch } from "redux";

import {
  EntryConstants,
  Entry,
  GetEntriesForVirtueAction,
  GetEntryAction,
  EntryForCreate,
  CreateEntryAction,
} from "./entry.types";

import { getAuthHeader } from "../../helpers/get-auth-header";

const ENTRY_BASE_URL = "/api/entries";
const VIRTUE_BASE_URL = "/api/virtues"

const authHeader = getAuthHeader();

export const getEntriesForVirtue = (virtueId: number) => async (
  dispatch: Dispatch
) => {
  try {
    const res = await axios.get<Entry[]>(VIRTUE_BASE_URL, {
      headers: authHeader
    });

    dispatch<GetEntriesForVirtueAction>({
      type: EntryConstants.GET_ENTRIES_FOR_VIRTUE,
      payload: res.data
    });
  } catch (err) {
    throw new Error(err);
  }
}

export const getEntry = (entryId: number) => async (
  dispatch: Dispatch
) => {
  try {
    const res = await axios.get<Entry>(`${ENTRY_BASE_URL}/${entryId}`, {
      headers: authHeader
    });

    dispatch<GetEntryAction>({
      type: EntryConstants.GET_ENTRY,
      payload: res.data
    });
  } catch (err) {
    throw new Error(err);
  }
}

export const createEntry = (entryToCreate: EntryForCreate) => async(
  dispatch: Dispatch
) => {
  try {
    const res = await axios.post<Entry>(ENTRY_BASE_URL, entryToCreate, {
      headers: authHeader
    });

    dispatch<CreateEntryAction>({
      type: EntryConstants.CREATE_ENTRY,
      payload: res.data
    });
  } catch (err) {
    return Promise.reject(err);
  }
}