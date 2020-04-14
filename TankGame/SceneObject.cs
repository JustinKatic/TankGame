using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathClasses;

namespace TankGame
{
    class SceneObject
    {
        //parent object
        protected SceneObject parent = null;
        //list of children objects
        protected List<SceneObject> children = new List<SceneObject>();
        //two transforms to our SceneObject, setting them both to the Identity
        public Matrix3 localTransform = new Matrix3();
        public Matrix3 globalTransform = new Matrix3();

        //properties to the class so that external code can access the transforms for reading
        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }
        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }

        //return parent
        public SceneObject Parent
        {
            get { return parent; }
        }

        //return number of children
        public int GetChildCount()
        {
            return children.Count;
        }
        
        //get child at index in paremnter
        public SceneObject GetChild(int index)
        {
            return children[index];
        }

        //adds child to parent object
        public void AddChild(SceneObject child)
        {
            // make sure it doesn't have a parent already
            Debug.Assert(child.parent == null);
            // assign "this as parent
            child.parent = this;
            // add new child to collection
            children.Add(child);
        }

        //removes child from parent 
        public void RemoveChild(SceneObject child)
        {
            if (children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        //When an object is destroyed that is currently within a hierarchy we need to inform its parent that it
        //should be removed.
        public SceneObject()
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }
            foreach (SceneObject so in children)
            {
                so.parent = null;
            }
        }

        public virtual void OnUpdate(float delatTime)
        {

        }
        public virtual void OnDraw()
        {

        }

       //ensure all child objects have their global transform updated as well, so we loop
       // through all children and call UpdateTransform() on them
        public void UpdateTransform()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            foreach (SceneObject child in children)
                child.UpdateTransform();
        }

        //updates all children
        public void Update(float deltaTime)
        {
            // run OnUpdate behaviour
            OnUpdate(deltaTime);
            // update children
            foreach (SceneObject child in children)
            {
                child.Update(deltaTime);
            }
        }

        //updates drawing on all children
        public void Draw()
        {
            // run OnDraw behaviour
            OnDraw();
            // draw children
            foreach (SceneObject child in children)
            {
                child.Draw();
            }
        }

        //sets initial position using translation mathClass
        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }
        //updates the position of translation with new x and y values
        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);
            UpdateTransform();
        }

        //sets rotation to absolute Z position
        public void SetRotateZ(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }
        //Rotates around current Z axis
        public void RotateZ(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }


        //sets scale of object
        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);
            UpdateTransform();
        }
        //updates scales of an object
        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);
            UpdateTransform();
        }
    }
}

