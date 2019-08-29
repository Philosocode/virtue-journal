import React, { Component } from "react";
import { Redirect, RouteComponentProps, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

import { AppState } from "../redux/store";
import { editVirtue, Virtue } from "../redux/virtue";
import { AxiosError, AxiosResponse } from "axios";

interface Props extends RouteComponentProps {
  editVirtue: Function,
  virtues: Virtue[]
}

interface RouteProps {
  virtueId?: string
}

class _VirtueEditPage extends Component<RouteComponentProps<RouteProps> & Props> {
  state = {
    virtueId: null,
    color: "",
    description: "",
    icon: "",
    name: "",
    error: "",
    shouldRedirectToVirtuesPage: false
  }

  componentDidMount() {
    const matchParams = this.props.match.params;

    if (!matchParams.virtueId) {
      console.log("matchParams.virtueId not found.");
      return;
    }

    const virtueId = Number.parseInt(matchParams.virtueId);
    const currentVirtue = this.props.virtues.find(v => v.virtueId === virtueId);

    if (!currentVirtue) {
      console.log("OH NO");
    } else {
      this.setState({
        virtueId: currentVirtue.virtueId,
        color: currentVirtue.color,
        description: currentVirtue.description,
        icon: currentVirtue.icon,
        name: currentVirtue.name
      });
    }
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSubmit = (event: React.SyntheticEvent) => {
    event.preventDefault();

    const { color, description, icon, name } = this.state;
    const virtueToEdit = { color, description, icon, name };

    this.props.editVirtue(this.state.virtueId, virtueToEdit)
      .then((res: AxiosResponse) => {
        this.setState({ shouldRedirectToVirtuesPage: true });
      })
      .catch((err: AxiosError) => {
        const errorResponse = err.response;

        if (errorResponse) {
          let errorString = `ERROR: ${errorResponse.data.title.slice(0, -1)}: `;

          Object.keys(errorResponse.data.errors).forEach((errorKey: string) => {
            errorString += ` ${errorKey}`;
          });

          this.setState({ error: errorString });
        }
      });
  }

  render() {
    return (
      <div>
        { this.state.shouldRedirectToVirtuesPage && <Redirect to="/virtues" /> }
        <h1>Virtue Edit Page</h1>
        <form onSubmit={this.handleSubmit}>

          <div className="form__group">
            <label htmlFor="name" className="form__label">Name:</label>
            <div className="form__input-container">
              <input
                required
                id="name"
                className="form__input"
                type="text"
                name="name"
                placeholder="Name"
                value={this.state.name}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="name" className="form__label">Color:</label>
            <div className="form__input-container">
              <input
                required
                id="color"
                className="form__input"
                type="text"
                name="color"
                placeholder="Color"
                value={this.state.color}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="icon" className="form__label">Icon:</label>
            <div className="form__input-container">
              <input
                required
                id="icon"
                className="form__input"
                type="text"
                name="icon"
                placeholder="Icon"
                value={this.state.icon}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="description" className="form__label">Description</label>
            <div className="form__input-container">
              <textarea
                id="description"
                className="form__input"
                name="description"
                placeholder="Description"
                value={this.state.description}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <button className="button" type="submit">Create</button>
          <div className="error error--server">{this.state.error}</div>
        </form>
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  virtues: state.virtue.virtues
});

export const VirtueEditPage = withRouter(connect(
  mapStateToProps,
  { editVirtue }
)(_VirtueEditPage));