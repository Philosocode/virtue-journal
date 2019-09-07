import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencilAlt, faTrash } from "@fortawesome/free-solid-svg-icons";

interface Props {
  color: string,
  createdAt: Date,
  description: string,
  icon: string,
  name: string,
  virtueId: number,
  handleEdit: (virtueId: number) => void,
  handleDelete: (virtueId: number) => void,
  handleClick: (virtueId: number) => void,
}

export const Virtue = (props: Props): JSX.Element => {
  const { color, createdAt, description, icon, name, handleClick, handleEdit, handleDelete, virtueId } = props;

  return (
    <div className="c-virtue" onClick={() => handleClick(virtueId)}>
      <h3 className="c-virtue__header">{virtueId} - {name}</h3>
      <div className="c-virtue__icons">
        <div className="c-virtue__edit" onClick={() => handleEdit(virtueId)}>
          <FontAwesomeIcon icon={faPencilAlt} />
        </div>
        <div className="c-virtue__delete" onClick={() => handleDelete(virtueId)}>
          <FontAwesomeIcon icon={faTrash} />
        </div>
      </div>

      <p className="c-virtue__description">{description}</p>
      <p className="c-virtue__metadata">{color} - {icon} - {createdAt}</p>
    </div>
  )
}