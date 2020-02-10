# TreeGrid002
WPFでTreeViewなDataGridを再現してみた

重要なのは以下の通り
* MainWindow.xamlで、DataGridRowのVisibilityを設定すること

```xaml:XAML
<Style TargetType="DataGridRow">
    <Setter Property="Visibility" Value="{Binding Path=VisibleState}" />
</Style>
```

```c#
public Visibility VisibleState => (parentItem?.IsChildOpen ?? true) ? Visibility.Visible : Visibility.Collapsed;
```


* 親子関係のあるデータ（親：子が１：Nなやつ）を元にRowデータを作成

```List<Person>```を元に```ObservableCollection<PersonItem>```を作成
Personの子要素もPersonItemで展開し、親の子要素として登録

* 親の子要素表示設定を取得して自分が表示対象かどうかを判定する

```c#
PersonItem.IsChildOpen
get => (parentItem?.IsChildOpen ?? true) && isChildOpen;
```

* 子要素の表示設定を変更したら再帰で子要素のプロパティチェンジを呼んでいく
子要素のプロパティチェンジ発火を一括で行うメソッドを用意し、
子要素表示設定を変更するたびに自分のプロパティチェンジを読んだ後に子要素の変更通知メソッドを呼ぶ
（子要素内でもさらに子要素の変更通知を呼ぶようにしてあるので子要素がなくなるまで呼び出される）

なお、ほぼほぼ基本機能しか使っていないので、ユーザーコントロールとして楽はできないけど、柔軟に対応できるはず

なんでTreeGrid002なのかっていうと、「[【WPF】DataGridでツリー表示を実現する](https://qiita.com/Nanao777/items/62c5a30ac312dde1e824)」が孫とか対応してないと思ったので試すために001を使用したから

また、上記リンクのものおよび、WinFormsの[TreeGridView Trick](https://www.codeproject.com/Tips/520388/TreeGridView-Trick)を過去のプロジェクトで使用されていたけど、子要素の非表示を実行すると行を削除する挙動になっていて、
色々と面倒なつくりになってた。
なので、WPF（ってかXAML）では行を非表示にするだけでデータの削除を行わなくて済むから変な行削除イベントが発生する心配がないよねという話


