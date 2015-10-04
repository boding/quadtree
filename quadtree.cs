using UnityEngine;

public class quadtree
{
    //  ---------
    // | nw | ne |
    // |----|----|
    // | sw | se |
    //  ---------
    public class node
    {
        public float x, y;
        public node ne, se, sw, nw;
        public Transform t;

        public node(float _x, float _y, Transform _t)
        {
            x = _x;
            y = _y;
            t = _t;
        }
    }

    public class rect
    {
        public float xMin, xMax, yMin, yMax;

        public rect(float _xMin, float _xMax, float _yMin, float _yMax)
        {
            xMin = _xMin;
            xMax = _xMax;
            yMin = _yMin;
            yMax = _yMax;
        }

        public bool contains(float x, float y)
        {
            return x >= xMin && x <= xMax && y >= yMin && y<= yMax;
        }
    }

    public node root;

    public void insert(float x, float y, Transform t)
    {
        root = insert(root, x, y, t);
    }

    node insert(node _node, float x, float y, Transform t)
    {
        if (null == _node)
        {
            return new node(x, y, t);
        }

        if (x >= _node.x && y >= _node.y)
        {
            _node.ne = insert(_node.ne, x, y, t);
        }
        else if (x >= _node.x && y <= _node.y)
        {
            _node.se = insert(_node.se, x, y, t);
        }
        else if (x <= _node.x && y <= _node.y)
        {
            _node.sw = insert(_node.sw, x, y, t);
        }
        else if (x <= _node.x && y >= _node.y)
        {
            _node.nw = insert(_node.nw, x, y, t);
        }

        return _node;
    }

    public void query(rect _rect)
    {
        query(root, _rect);
    }

    void query(node _node, rect _rect)
    {
        if (null == _node)
        {
            return;
        }

        /*
        if (_rect.contains(_node.x, _node.y))
        {
            _node.t.gameObject.renderer.material.color = Color.red;
        }
        else
        {
            _node.t.gameObject.renderer.material.color = Color.white;
        }
        */

        if (_rect.xMax >= _node.x && _rect.yMax >= _node.y)
        {
            query(_node.ne, _rect);
        }

        if (_rect.xMax >= _node.x && _rect.yMin <= _node.y)
        {
            query(_node.se, _rect);
        }

        if (_rect.xMin <= _node.x && _rect.yMin <= _node.y)
        {
            query(_node.sw, _rect);
        }

        if (_rect.xMin <= _node.x && _rect.yMax >= _node.y)
        {
            query(_node.nw, _rect);
        }
    }
}
