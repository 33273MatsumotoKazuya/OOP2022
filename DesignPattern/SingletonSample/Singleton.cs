using System;

public class Singleton {

    private static Singleton singleton = null;

    //コンストラクタ
    private Singleton() {                                 
        Console.WriteLine("インスタンスを生成しました。");
    }

    public static Singleton getInstance() {
        if (singleton == null) {
            singleton = new Singleton();
        }
        return singleton;
    }
}
