import React from 'react';
import _ from "lodash";
import { Button } from "@material-ui/core";

const styles = {
    main: {
        background: "#f44336",
        color: "#FFF"
    }
};

const texts = {
    delete: "Delete"
};

export default props => {
    const { loading, onDelete } = props;
    return (
        <Button style={styles.main} disabled={loading} onClick={onDelete}>{texts.delete}</Button>
    );
}