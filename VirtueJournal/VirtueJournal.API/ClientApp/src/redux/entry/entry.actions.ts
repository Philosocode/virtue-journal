import axios from "axios";
import { Dispatch } from "redux";

import {
  EntryConstants,
  Entry,
  GetEntriesForVirtueAction,
  GetEntryAction,
  EntryForCreate,
  CreateEntryAction,
  GetUncategorizedEntriesAction,
  DeleteEntryAction,
} from "./entry.types";

import { getAuthHeader } from "../../helpers/get-auth-header";

const ENTRY_BASE_URL = "/api/entries";
const VIRTUE_BASE_URL = "/api/virtues"

const authHeader = getAuthHeader();

export const getEntriesForVirtue = (virtueId: number) => async (
  dispatch: Dispatch
) => {
  try {
    const res = await axios.get<Entry[]>(`${VIRTUE_BASE_URL}/${virtueId}/entries`, {
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

export const getUncategorizedEntries = () => async (dispatch: Dispatch) => {
  try {
    const res = await axios.get<Entry[]>(`${ENTRY_BASE_URL}/uncategorized`, {
      headers: authHeader
    });

    dispatch<GetUncategorizedEntriesAction>({
      type: EntryConstants.GET_UNCATEGORIZED_ENTRIES,
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

export const createEntry = (entryToCreate: EntryForCreate) => async (
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

export const deleteEntry = (entryId: number) => async (dispatch: Dispatch) => {
  try {
    await axios.delete(`${ENTRY_BASE_URL}/${entryId}`, {
      headers: authHeader
    });

    dispatch<DeleteEntryAction>({
      type: EntryConstants.DELETE_ENTRY,
      payload: entryId
    });
  } catch (err) {
    return Promise.reject(err);
  }
};