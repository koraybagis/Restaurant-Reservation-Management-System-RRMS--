-- Test kullanıcıları ekleme scripti
-- Koray Bağış (Garson) ve Cenker Akmaz (TemizlikPersoneli)

-- Koray Bağış - Garson
IF NOT EXISTS (SELECT * FROM users WHERE username = 'koray')
BEGIN
    INSERT INTO users (username, password, email, full_name, phone, role, IsBanned) 
    VALUES ('koray', '123456', 'koray@test.com', 'Koray Bağış', '5551234567', 'Garson', 0);
    PRINT 'Koray Bağış başarıyla eklendi.';
END
ELSE
BEGIN
    PRINT 'Koray Bağış zaten mevcut.';
END

-- Cenker Akmaz - TemizlikPersoneli
IF NOT EXISTS (SELECT * FROM users WHERE username = 'cenker')
BEGIN
    INSERT INTO users (username, password, email, full_name, phone, role, IsBanned) 
    VALUES ('cenker', '123456', 'cenker@test.com', 'Cenker Akmaz', '5559876543', 'TemizlikPersoneli', 0);
    PRINT 'Cenker Akmaz başarıyla eklendi.';
END
ELSE
BEGIN
    PRINT 'Cenker Akmaz zaten mevcut.';
END

PRINT 'Test kullanıcıları ekleme tamamlandı.';
