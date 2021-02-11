using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; 
#endif


[System.Serializable]
public class Point
{
    [SerializeField]
    private int _x, _y;

    public Point(int newX, int newY)
    {
        _x = newX;
        _y = newY;
    }

    //---[  PROPERTIES   ]--------------------------------------------//
    public static Point Zero => new Point(0, 0);
    public static Point One => new Point(1, 1);
    public static Point Up => new Point(0, 1);
    public static Point Down => new Point(0, -1);
    public static Point Right => new Point(1, 0);
    public static Point Left => new Point(-1, 0);
    
    public int X
    {
        get => _x; 
        set => _x = value;
    }

    public int Y
    {
        get => _y; 
        set => _y = value;
    }
    

    //---[   OPERATORS   ]--------------------------------------------//
    public static Point operator -(Point point)
    {
        return new Point(-point.X, -point.Y);
    }
    
    public static Point operator -(Point point1, Point point2)
    {
        return new Point(point1.X - point2.X, point1.Y - point2.Y);
    }
    
    public static Point operator +(Point point1, Point point2)
    {
        return new Point(point1.X + point2.X, point1.Y + point2.Y);
    }
    
    public static Point operator *(Point point1, Point point2)
    {
        return new Point(point1.X * point2.X, point1.Y * point2.Y);
    }
    
    public static Point operator *(Point point, int multiplier)
    {
        return new Point(point.X * multiplier, point.Y * multiplier);
    }

    //---[STATIC  METHODS]--------------------------------------------//
    public static Point FromVector(Vector2 vector) => 
        new Point((int)vector.x, (int)vector.y);

    public static Point FromVector(Vector3 vector) => 
        new Point((int)vector.x, (int)vector.y);

    public static Point Multiply(Point point, int m) => 
        new Point(point.X * m, point.Y * m);

    public static Point Add(Point point1, Point point2) => 
        new Point(point1.X + point2.X, point1.Y + point2.Y);

    public static Point Clone(Point point) => 
        new Point(point.X, point.Y);

    
    //---[NON-STATIC  METHODS]----------------------------------------//
    public void Multiply(int m)
    {
        X *= m;
        Y *= m;
    }
	
    public void Add(Point point)
    {
        X += point.X;
        Y += point.Y;
    }
	
    public Vector2 ToVector() => 
        new Vector2(X, Y);
    
    public override bool Equals(object other)
    {
        if (other is Point point)
            return (point.X == X && point.Y == Y);

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}



#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Point))]
public class PointEditor : PropertyDrawer
{
    private const float Spacing = 5f;
    private const float LabelWidth = 12f;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect rextX = new Rect(
            position.x, 
            position.y, 
            position.width / 2 - LabelWidth - (Spacing / 2.0f), 
            position.height);
        
        Rect rextY = new Rect(
            position.x + rextX.width + Spacing + LabelWidth, 
            position.y, 
            position.width / 2 - LabelWidth - (Spacing / 2.0f), 
            position.height);
        
        // Отрисовка Х
        EditorGUI.LabelField(rextX, "X");
        rextX.x += LabelWidth;
        EditorGUI.PropertyField(rextX, property.FindPropertyRelative("_x"), GUIContent.none);

        // Отрисовка Y
        EditorGUI.LabelField(rextY, "Y");
        rextY.x += LabelWidth;
        EditorGUI.PropertyField(rextY, property.FindPropertyRelative("_y"), GUIContent.none);
        
        EditorGUI.indentLevel = indent;
    }
}
#endif