unit events4u;

type TProc = procedure(sender: object);

type TClass = class
event OnClick, OnClick2 : TProc;
procedure MyProc(sender : object);
begin
writeln(9);
end;
constructor Create;
begin
OnClick += MyProc;
OnClick(self);
OnClick -= MyProc;
end;
procedure RaiseMethod;
begin
OnClick(self);
end;
end;

procedure MyProc(sender : object);
begin
writeln(5);
end;

end.