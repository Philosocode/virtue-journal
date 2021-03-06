export enum VirtueConstants {
  GET_VIRTUE = "GET_VIRTUE",
  GET_VIRTUES = "GET_VIRTUES",
  CREATE_VIRTUE = "CREATE_VIRTUE",
  CREATE_VIRTUE_FAILED = "CREATE_VIRTUE_FAILED",
  EDIT_VIRTUE = "EDIT_VIRTUE",
  DELETE_VIRTUE = "DELETE_VIRTUE",
  SET_CURRENT_VIRTUE = "SET_CURRENT_VIRTUE"
}

export interface Virtue {
  virtueId: number,
  name: string,
  color: string,
  description: string,
  icon: string,
  createdAt: Date
}

export interface VirtueForCreate {
  color: string,
  description: string,
  icon: string,
  name: string
}

export interface VirtueForEdit {
  color?: string,
  description?: string,
  icon?: string,
  name?: string
}

export interface VirtueState {
  currentVirtue?: Virtue
  virtues: Virtue[]
}

/* ACTIONS */
export interface GetVirtuesAction {
  type: VirtueConstants.GET_VIRTUES,
  payload: Virtue[]
}

export interface GetVirtueAction {
  type: VirtueConstants.GET_VIRTUE,
  payload: Virtue
}

export interface CreateVirtueAction {
  type: VirtueConstants.CREATE_VIRTUE,
  payload: Virtue
}

export interface EditVirtueAction {
  type: VirtueConstants.EDIT_VIRTUE
}

export interface DeleteVirtueAction {
  type: VirtueConstants.DELETE_VIRTUE,
  payload: number
}

export interface SetCurrentVirtueAction {
  type: VirtueConstants.SET_CURRENT_VIRTUE,
  payload: Virtue
}

export type VirtueAction = (
  GetVirtuesAction | GetVirtueAction | CreateVirtueAction | 
  EditVirtueAction | DeleteVirtueAction | SetCurrentVirtueAction
);