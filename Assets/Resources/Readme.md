# データ規則
## ファイル名について
 `{name}/{rl}_{position}_{area}_{height}.csv`
- name:名前
- rl: 右足左足どちらか
- position: 突起の場所
  1. 親指の腹
  2. 親指付け根
  3. 中指付け根
  4. 小指付け根
  5. かかと
- area: 1辺の突起の個数（n→$4n*4n \; (mm^2)$）
- height: 重ねたブロックの枚数 (n→3n+1.5 mm)
## 中身について
- １つのファイルに1条件、複数試行入っている
- 並び順は,x,y,z(position),x,y,z,w(rotation)