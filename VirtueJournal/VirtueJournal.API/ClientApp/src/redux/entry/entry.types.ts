// Import actions
export interface Entry {
  entryId: number,
  title: string,
  description: string,
  createdAt: Date,
  lastEdited: Date,
  starred: boolean
}

// public ICollection<VirtueEntryGetDto> VirtueLinks { get; set; } = new List<VirtueEntryGetDto>();

export enum EntryActionTypes {
  TEST = "TEST",
  WOW = "WOW"
};

/*
export const SEND_MESSAGE = 'SEND_MESSAGE'
export const DELETE_MESSAGE = 'DELETE_MESSAGE'

interface SendMessageAction {
  type: typeof SEND_MESSAGE
  payload: Message
}

interface DeleteMessageAction {
  type: typeof DELETE_MESSAGE
  meta: {
    timestamp: number
  }
}

export type ChatActionTypes = SendMessageAction | DeleteMessageAction
*/