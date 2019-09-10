export enum EntryConstants {
  GET_ENTRIES_FOR_VIRTUE = "GET_ENTRIES_FOR_VIRTUE",
  GET_UNCATEGORIZED_ENTRIES = "GET_UNCATEGORIZED_ENTRIES",
  GET_ENTRY = "GET_ENTRY",
  CREATE_ENTRY = "CREATE_ENTRY",
  EDIT_ENTRY = "EDIT_ENTRY",
  DELETE_ENTRY = "DELETE_ENTRY"
};

export interface Entry {
  entryId: number,
  title: string,
  description: string,
  createdAt: Date,
  lastEdited: Date,
  starred: boolean,
  virtueLinks?: VirtueLink[]
}

export interface EntryForCreate {
  title: string,
  description: string,
  createdAt: Date,
  starred: boolean
  virtueLinks?: VirtueLink[]
}

export interface EntryForEdit extends EntryForCreate {}

export interface VirtueLink {
  virtueId: number,
  difficulty: Difficulty
}

export enum Difficulty {
  VeryEasy,
  Easy,
  Medium,
  Hard,
  VeryHard
}

export interface EntryState {
  currentEntry?: Entry,
  entries: Entry[]
}

/* ACTIONS */
export interface GetEntriesForVirtueAction {
  type: EntryConstants.GET_ENTRIES_FOR_VIRTUE,
  payload: Entry[]
}

export interface GetUncategorizedEntriesAction {
  type: EntryConstants.GET_UNCATEGORIZED_ENTRIES,
  payload: Entry[]
}

export interface GetEntryAction {
  type: EntryConstants.GET_ENTRY,
  payload: Entry
}

export interface CreateEntryAction {
  type: EntryConstants.CREATE_ENTRY,
  payload: Entry
}

export interface EditEntryAction {
  type: EntryConstants.EDIT_ENTRY
}

export interface DeleteEntryAction {
  type: EntryConstants.DELETE_ENTRY,
  payload: number
}

export type EntryAction = (
  GetEntriesForVirtueAction | GetUncategorizedEntriesAction | GetEntryAction | 
  CreateEntryAction | DeleteEntryAction
);