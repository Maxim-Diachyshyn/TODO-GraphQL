import React from 'react';
import _ from "lodash";
import { Mutation, Subscription, Query } from "react-apollo";
import { Button } from "react-bootstrap";
import { deleteTodo } from "../../Board/mutations";

export default props => {
    const { id, loading, onDelete } = props;
    return (
        <Button disabled={loading} variant="danger" onClick={onDelete}>Delete Task</Button>
    );
}